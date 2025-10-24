using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR_system.Models
{
    public class NCRModels
    {
        private int _RecordID;
        private string _DateIssued;
        private string _RegNo;
        private string _Category;
        private string _IssueGroup;
        private string _ModelNo;
        private int _Quantity;
        private string _Contents;
        private string _CircularStatus;
        private string _DateCloseReg;
        private int _Status;
        private int _SectionID;
        private string _FilePath;
        private string _TargetDate;
        private int _Process;
        private string _DateRegist;


        public int RecordID
        {
            get => _RecordID;
            set => _RecordID = value;
        }

        public int SectionID
        {
            get => _SectionID;
            set => _SectionID = value;
        }
        public string DateIssued
        {
            get => _DateIssued;
            set => _DateIssued = value;
        }
        public string RegNo
        {
            get => _RegNo;
            set => _RegNo = value;
        }
        public string Category
        {
            get => _Category;
            set => _Category = value;
        }
        public string IssueGroup
        {
            get => _IssueGroup;
            set => _IssueGroup = value;
        }
     
        public string ModelNo
        {
            get => _ModelNo;
            set => _ModelNo = value;
        }
        public int Quantity
        {
            get => _Quantity;
            set => _Quantity = value;
        }
        public string Contents
        {
            get => _Contents;
            set => _Contents = value;
        }
        public string CircularStatus
        {
            get => _CircularStatus;
            set => _CircularStatus = value;
        }
        public string DateCloseReg
        {
            get => _DateCloseReg;
            set => _DateCloseReg = value;
        }
        public int Status
        {
            get => _Status;
            set => _Status = value;
        }
        public string FilePath
        {
            get => _FilePath;
            set => _FilePath = value;
        }
        public int Process
        {
            get => _Process;
            set => _Process = value;
        }
        public string TargetDate
        {
            get => _TargetDate;
            set => _TargetDate = value;
        }
        public string DateRegist
        {
            get => _DateRegist;
            set => _DateRegist = value;
        }
    }
    public class CustomerModel
    {
        private int _RecordID;
        private string _DateCreated;
        private string _ModelNo;
        private string _LotNo;
        private int _NGQty;
        private string _Details;
        private string _RegNo;
        private string _CustomerName;
        private int _SectionID;
        private int _Status;
        private int _CCtype;


        public int RecordID
        {
            get => _RecordID;
            set => _RecordID = value;
        }
        public string DateCreated
        {
            get => _DateCreated;
            set => _DateCreated = value;
        }
        public string ModelNo
        {
            get => _ModelNo;
            set => _ModelNo = value;
        }
        public string LotNo
        {
            get => _LotNo;
            set => _LotNo = value;
        }
        public int NGQty
        {
            get => _NGQty;
            set => _NGQty = value;
        }
        public string Details
        {
            get => _Details;
            set => _Details = value;
        }
        public string RegNo
        {
            get => _RegNo;
            set => _RegNo = value;
        }
        public string CustomerName
        {
            get => _CustomerName;
            set => _CustomerName = value;
        }

        public int SectionID
        {
            get => _SectionID;
            set => _SectionID = value;
        } 
        public int Status
        {
            get => _Status;
            set => _Status = value;
        }
        public int CCtype
        {
            get => _CCtype;
            set => _CCtype = value;
        }
    }
    public class InprocessModel
    {
        private int _RecordID;
        private string _DateEncounter;
        private string _TitleEmail;
        private int _Shift;
        private string _Line;
        private string _Model;
        private string _ShopOrder;
        private string _Defect;
        private int _NGQty;
        private string _ProcEncounter;
        private string _cause;
        private string _SectionDep;
        private string _Invest;
        private int _Status;
        private string _P1saStatus;
        private string _Remarks;
        private int _SectionID;

        public int RecordID
        {
            get => _RecordID;
            set => _RecordID = value;
        }
        public string DateEncounter
        {
            get => _DateEncounter;
            set => _DateEncounter = value;
        }
        public string TitleEmail
        {
            get => _TitleEmail;
            set => _TitleEmail = value;
        }
        public int Shift
        {
            get => _Shift;
            set => _Shift = value;
        }
        public string Line
        {
            get => _Line;
            set => _Line = value;
        }
        public string Model
        {
            get => _Model;
            set => _Model = value;
        }
        public string ShopOrder
        {
            get => _ShopOrder;
            set => _ShopOrder = value;
        }
        public string Defect
        {
            get => _Defect;
            set => _Defect = value;
        }

        public int NGQty
        {
            get => _NGQty;
            set => _NGQty = value;
        }
        public string ProcEncounter
        {
            get => _ProcEncounter;
            set => _ProcEncounter = value;
        }
        public string cause
        {
            get => _cause;
            set => _cause = value;
        }
        public string SectionDep
        {
            get => _SectionDep;
            set => _SectionDep = value;
        }

        public string Invest
        {
            get => _Invest;
            set => _Invest = value;
        }
        public string P1saStatus
        {
            get => _P1saStatus;
            set => _P1saStatus = value;
        }

        public string Remarks
        {
            get => _Remarks;
            set => _Remarks = value;
        }
        public int Status
        {
            get => _Status;
            set => _Status = value;
        }
        public int SectionID
        {
            get => _SectionID;
            set => _SectionID = value;
        }
    }
    public class RejectShipmentModel
    {
        private int _RecordID;
        private string _DateIssued;
        private string _RegNo;
        private string _IssueGroup;
        private int _SectionID;
        private string _ModelNo;
        private int _Quantity;
        private string _Contents;
        private string _DateCloseReg;
        private int _Status;
        private int _Process;


        public int RecordID
        {
            get => _RecordID;
            set => _RecordID = value;
        }
        public string DateIssued
        {
            get => _DateIssued;
            set => _DateIssued = value;
        }
        public string RegNo
        {
            get => _RegNo;
            set => _RegNo = value;
        }
       
        public string IssueGroup
        {
            get => _IssueGroup;
            set => _IssueGroup = value;
        }
        public int SectionID
        {
            get => _SectionID;
            set => _SectionID = value;
        }
        public string ModelNo
        {
            get => _ModelNo;
            set => _ModelNo = value;
        }
        public int Quantity
        {
            get => _Quantity;
            set => _Quantity = value;
        }
        public string Contents
        {
            get => _Contents;
            set => _Contents = value;
        }
        public string DateCloseReg
        {
            get => _DateCloseReg;
            set => _DateCloseReg = value;
        }
        public int Process
        {
            get => _Process;
            set => _Process = value;
        }
        public int Status
        {
            get => _Status;
            set => _Status = value;
        }
    }

}
