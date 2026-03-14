import {
    getMethod,
    postMethod, 
    getMethodPublic, 
    postMethodPublic
} from '../Modules/index.js';



class MoldieDailyController {
    constructor() {
        this.isFirstLoad = true;
        this.isYearLoad = true;
        this.isSeletedSearch = 0;

        this.DailyData = [];

        this.Tablebody = document.getElementById("DieMoldDaily");
        this.Tablebody = document.getElementById("dieMoldMasterlist");
        // For Inistalize parameter Load Data first
        this.processID = document.getElementById("DailyProcess");
        this.MonthText = new Date().getMonth() + 1;
        this.GetRow = 0;

        this.yearSelect = document.getElementById("yearselectDaily");
    }

    async init() {
        await this.GetMoldDieDaily();
        this.BindEvents();
    }

    BindEvents() {

    }

    async GetMoldDieDaily() {

        // Display first the Year
        if (this.isYearLoad) {
           await this.GetMoldieYear();
        }

        const yearstr = this.yearSelect.value === null || this.yearSelect.value === ""
            ? new Date().getFullYear()
            : this.yearSelect.value;

        let res = await getMethod('/Mold/GetMoldDieDailyList', {
             Months: this.MonthText,
             Days: this.GetRow,
             Year: yearstr,
             ProcessID: this.processID.value
        });

        this.isYearLoad = false;
    }

    async GetMoldieYear() {
        const res = await getMethodPublic('/Mold/GetYearMoldDie', {});
        this.yearSelect.innerHTML = "";

        res.forEach(year => {
            const option = document.createElement("option");
            option.value = year;
            option.textContent = year;
            this.yearSelect.appendChild(option);
        });

        this.yearSelect.selectedIndex = 0;
    }
};


///* ================= INIT ================= */
export function initMoldiePage() {
    const controller = new MoldieDailyController();
    controller.init();
}