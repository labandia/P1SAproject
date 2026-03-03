import {
    getMethod,
    postMethod,
    initLazyImages
} from '../Modules/index.js';

class InventoryController {
    constructor() {
        this.inventList = [];
        this.tableBody = null;
    }

    async init() {
        console.log("adsd");
        //this.tableBody = document.getElementById("InventorylistData");
        //this.bindEvents();
        await this.loadPageData();
    }

    //bindEvents() {
    //    const searchBox = document.getElementById("searchbox");
    //    const categorySelect = document.getElementById("Categoryselect");

    //    if (searchBox) {
    //        searchBox.addEventListener("input", () => this.applyFilters());
    //    }

    //    if (categorySelect) {
    //        categorySelect.addEventListener("change", () => this.applyFilters());
    //    }
    //}

    async loadInventory() {
        const result = await getMethod("/Hydro/GetHydroInventory");

        if (!result?.Success) return;
        console.log(result);
        this.inventList = result.data;
        //this.renderTable(this.inventList);
    }

    //renderTable(data) {
    //    if (!this.tableBody) return;

    //    this.tableBody.innerHTML = "";

    //    const fragment = document.createDocumentFragment();

    //    data.forEach(row => {
    //        const tr = document.createElement("tr");
    //        tr.innerHTML = `
    //            <td>${row.PartName}</td>
    //            <td>${row.PartNo}</td>
    //            <td>${row.CurrentQty}</td>
    //        `;
    //        fragment.appendChild(tr);
    //    });

    //    this.tableBody.appendChild(fragment);
    //}

    //applyFilters() {
    //    const searchValue = document
    //        .getElementById("searchbox")
    //        ?.value
    //        .toLowerCase() || "";

    //    const filtered = this.inventList.filter(item =>
    //        item.PartName?.toLowerCase().includes(searchValue)
    //    );

    //    this.renderTable(filtered);
    //}

    async loadPageData() {
        await this.loadInventory();
    }
}

export function initInventoryPage() {
    const controller = new InventoryController();
    controller.init();
}