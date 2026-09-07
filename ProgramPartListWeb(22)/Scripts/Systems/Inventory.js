import {
    getMethod,
    postMethod,
    initLazyImages
} from '../Modules/index.js';



class InventoryController {
    constructor() {
        this.inventList = [];
        this.tableBody = null;
        this.editModal = null;
    }

    async init() {
        this.tableBody = document.getElementById("InventorylistData");

        if (!this.tableBody) {
            console.warn("InventorylistData not found.");
            return;
        }

        const modalElement = document.getElementById("EditModal");

        if (!modalElement) {
            console.warn("editModal not found.");
            return;
        }

        if (typeof bootstrap === "undefined") {
            console.error("Bootstrap JS is not loaded.");
            return;
        }

        this.editModal = new bootstrap.Modal(modalElement);

        this.tableBody.addEventListener("click", (e) => {
            const editBtn = e.target.closest(".Editbtnbutton");
            if (!editBtn) return;

            e.preventDefault();

            // 🔥 Get ID from button
            const stockId = editBtn.dataset.id;

            console.log("Clicked StockID:", stockId);

            const part = this.inventList.find(p => p.StockID == stockId);
            console.log(part);
            if (part) {
                this.openEditModal(part);
            }
        });

        this.bindEvents();
        await this.loadPageData();
    }

    bindEvents() {
        const searchBox = document.getElementById("searchbox");
        const categorySelect = document.getElementById("Categoryselect");

        if (searchBox) {
            searchBox.addEventListener("input", () => this.applyFilters());
        }

        if (categorySelect) {
            categorySelect.addEventListener("change", () => this.applyFilters());
        }
    }

    async loadInventory() {
        const result = await getMethod("/Hydro/GetHydroInventory");

        if (!result?.Success) return;
        this.inventList = result.data;
        console.log(this.inventList);
        this.renderTable(this.inventList);
    }

    renderTable(data) {
        if (!this.tableBody) return;

        this.tableBody.innerHTML = "";

        const fragment = document.createDocumentFragment();

        data.forEach(row => {
            const qty = this.formatQuantity(row.CurrentQty, row.Unit);
            const warn = this.formatQuantity(row.WarningLevel, row.Unit);

            const imagePath = row.ImageParts
                ? `/Hydro/DisplaytheImage?filename=${encodeURIComponent(row.ImageParts)}`
                : "/Content/Images/no-image.png";

            const incrementBtn = document.createElement("button");
            // For add class and styling
            incrementBtn.innerHTML = `<i class="fa-solid fa-plus"></i>`;

            const decrementBtn = document.createElement("button");
            decrementBtn.innerHTML = `<i class="fa-solid fa-minus"></i>`;

            incrementBtn.addEventListener("click", () =>
                this.incrementPart(row.StockID, row.CurrentQty, row.ReorderLevel, row.Unit)
            );

            decrementBtn.addEventListener("click", () =>
                this.decrementPart(row.StockID, row.CurrentQty, row.ReorderLevel, row.Unit)
            );

            const tr = document.createElement("tr");
            tr.dataset.id = row.PartID;
            tr.dataset.category = row.CategoryID;
            tr.classList.add("rowClick");


            tr.innerHTML = `
                    <td style="text-align:left;padding-left:1.5em;">
                        <div class="flex_align" style="gap:15px;">
                            <img class="NoImages lazy"
                                 src="/Content/Images/no-image.png"
                                 data-src="${imagePath}"
                                 loading="lazy" />
                            <div>
                                <p style="color:#222;font-weight:600;">
                                    ${row.PartName || "-"}
                                </p>
                                <small>${row.PartNo || "-"}</small>
                            </div>
                        </div>
                    </td>
                    <td>${row.Supplier || "-"}</td>
                    <td style="font-weight:600;color:#185d8b;"></td>
                    <td style="font-weight:600;">${warn}</td>
                    <td>
                          <div class="${this.getStatusClass(row.Status)}">
                                ${row.Status}
                            </div>
                    </td>
                    <td>
                        <div class="flex_center">
                            <button class="Editbtnbutton text-primary bg-white"  data-id="${row.StockID}">
                                <i class="fa-solid fa-pen-to-square"></i>
                            </button>
                        </div>
                    </td>
                `;

            // Insert quantity + buttons properly
            const qtyCell = tr.children[2];
            qtyCell.appendChild(decrementBtn);
            qtyCell.insertAdjacentHTML("beforeend", qty);
            qtyCell.appendChild(incrementBtn);


            fragment.appendChild(tr);
        });

        this.tableBody.appendChild(fragment);

        // 🔥 Run lazy load once (not per row)
        this.initLazyLoad();
    }

    openEditModal(part) {
        if (!part) return;

        const setValue = (id, value, fallback = "") => {
            const el = document.getElementById(id);
            if (!el) return;
            el.value = value ?? fallback;
        };

        // Text fields
        setValue("TempPartno", part.PartNo);
        setValue("EditPartNo", part.PartNo);
        setValue("EditPartName", part.PartName);

        // Select dropdown (important: convert to string)
        setValue("EditCategoryID", part.CategoryID != null ? String(part.CategoryID) : "");

        // Numbers
        setValue("EditReorderLevel", part.ReorderLevel, 0);
        setValue("EditWarningLevel", part.WarningLevel, 0);
        setValue("EditUnit_Price", part.Unit_Price, 0);

        // Optional text
        setValue("EditUnit", part.Unit);
        setValue("EditSupplier", part.Supplier);

        // Hidden image field
        setValue("ExistingImageParts", part.ImageParts);

        // Image preview logic
        const previewContainer = document.getElementById("EditpreviewContainer");
        const previewImage = document.getElementById("EditpreviewImage");

        if (previewContainer && previewImage) {

            if (part.ImageParts) {

                const imagePath =
                    `/Hydro/DisplaytheImage?filename=${encodeURIComponent(part.ImageParts)}`;

                previewImage.src = imagePath;
                previewImage.onerror = () =>
                    previewImage.src = "/Content/Images/no-image.png";

                previewContainer.style.display = "block";

            } else {

                previewImage.src = "/Content/Images/no-image.png";
                previewContainer.style.display = "none";
            }
        }


        this.editModal.show();
    }

    async savePart() {

        const id = document.getElementById("modalPartId").value;
        const name = document.getElementById("modalPartName").value;
        const supplier = document.getElementById("modalSupplier").value;

        const result = await postMethod("/Hydro/UpdatePart", {
            PartID: id,
            PartName: name,
            Supplier: supplier
        });

        if (result?.Success) {
            this.editModal.hide();
            await this.loadInventory();
        }
    }


    safeValue(value, fallback = "") {
        return value === null || value === undefined ? fallback : value;
    }


    formatQuantity(qty, unit) {
        if (qty == null) return "-";
        const formatted = Number(qty).toLocaleString();
        return unit ? `${formatted} ${unit}` : formatted;
    }

    incrementPart(stockID, CurrentQty, ReorderLevel, Unit) {
        alert("Increase" + stockID);
    }
    decrementPart(StockID, CurrentQty, ReorderLevel, Unit){
        alert("Decrease" + StockID);
    }

    getStatusClass(status) {
        switch (status) {
            case "Sufficient": return "StocksGood";
            case "Out of stocks": return "Low";
            case "Restock": return "Warning";
            default: return "Incomplete";
        }
    }

    applyFilters() {
        const searchValue = document
            .getElementById("searchbox")
            ?.value
            .toLowerCase() || "";

        const filtered = this.inventList.filter(item =>
            item.PartName?.toLowerCase().includes(searchValue)
        );

        this.renderTable(filtered);
    }

    async loadPageData() {
        await this.loadInventory();
    }

    initLazyLoad() {
        const images = this.tableBody.querySelectorAll("img.lazy[data-src]");

        images.forEach(img => {
            const realSrc = img.dataset.src;

            img.onload = () => img.classList.add("loaded");
            img.onerror = () => img.src = "/Content/Images/no-image.png";

            img.src = realSrc;
            img.removeAttribute("data-src");
        });
    }
}

export function initInventoryPage() {
    const controller = new InventoryController();
    controller.init();
}