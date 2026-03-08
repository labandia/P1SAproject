const activeTimers = [];



window.GetMetalMaskTransaction = async function () {
    // STOP old timers
    activeTimers.forEach(clearInterval);
    activeTimers.length = 0;


    let res = await fetchData('/Circuit/MetalMask/GetMetalMaskInformation', { Stats: 1 });
    $("#Maskcardcontainer").empty();

    if (res?.Success) {

        let TableData = res.Data;
        let html = "";

        res.Data.forEach(rowData => {

            html += `
                                <div class="Maskcard" data-id="${rowData.RecordID}">
                                    <div class="MaskHeader">
                                        <h5>Metal mask rev partnum : <span>${rowData.Partnumber}</span></h5>
                                    </div>

                                    <div class="MaskContent">
                                        <div><label>SMT Line</label><p>${rowData.SMTLine}</p></div>
                                        <div><label>Area</label><p>${rowData.AREA}</p></div>
                                        <div><label>Shift</label><p>${rowData.Shift != false ? "DS" : "NS"}</p></div>
                                        <div><label>Blocks</label><p>${rowData.Blocks}</p></div>
                                    </div>

                                    <div class="MaskContent">
                                        <div>
                                            <label>Start Time</label>
                                             <p>${rowData.SMT_start === "00:00" ? "--:--" : rowData.SMT_start}</p>
                                        </div>

                                        <div>
                                            <label>End Time</label>
                                           <p>${rowData.SMT_end === "00:00" ? "--:--" : rowData.SMT_end}</p>
                                        </div>

                                        <div>
                                            <label>Total Time</label>
                                              <p class="total-time">${rowData.TotalTimeHHMM}</p>
                                        </div>
                                    </div>
                                </div>`;
        });

        $("#Maskcardcontainer").append(html);



        /* ============================
              START REALTIME TIMERS
           ============================ */

        document.querySelectorAll(".Maskcard").forEach((card, i) => {

            const row = res.Data[i];

            // Only run if still active
            if (row.SMT_end !== "00:00") return;
            const changeDate = formatJsonDate(row.DateInput);
            const startDate = parseTime(changeDate, row.SMT_start);
            const timer = startRealtimeTimer(card, changeDate, row.SMT_start);

            activeTimers.push(timer);
        });


    }
};