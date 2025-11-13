using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootWristStrapsAnalysis.Model
{
    public class IFootWristModel
    {
        // Private fields
        private int _recordId;
        private DateTime? _testDate;
        private TimeSpan? _testTime;
        private string _employeeId;
        private string _employeeName;
        private bool _comprehensiveResult;
        private string _leftFootResistance;
        private bool _leftFootResult;
        private string _rightFootResistance;
        private bool _rightFootResult;
        private string _wristStrapResult;
        private string _conductivityEvaluation;
        private string _lowerEvaluationLimit;
        private string _upperEvaluationLimit;
        private bool _evaluationBuzzer;
        private bool _evaluationExternalOutput;
        private string _fg470;
        private bool _note;

        // Public properties (encapsulated)
        public int RecordID
        {
            get => _recordId;
            set => _recordId = value;
        }

        public DateTime? TestDate
        {
            get => _testDate;
            set => _testDate = value;
        }

        public TimeSpan? TestTime
        {
            get => _testTime;
            set => _testTime = value;
        }

        public string EmployeeID
        {
            get => _employeeId;
            set => _employeeId = value;
        }

        public string EmployeeName
        {
            get => _employeeName;
            set => _employeeName = value;
        }

        public bool ComprehensiveResult
        {
            get => _comprehensiveResult;
            set => _comprehensiveResult = value;
        }

        public string LeftFootResistance
        {
            get => _leftFootResistance;
            set => _leftFootResistance = value;
        }

        public bool LeftFootResult
        {
            get => _leftFootResult;
            set => _leftFootResult = value;
        }

        public string RightFootResistance
        {
            get => _rightFootResistance;
            set => _rightFootResistance = value;
        }

        public bool RightFootResult
        {
            get => _rightFootResult;
            set => _rightFootResult = value;
        }

        public string WristStrapResult
        {
            get => _wristStrapResult;
            set => _wristStrapResult = value;
        }

        public string ConductivityEvaluation
        {
            get => _conductivityEvaluation;
            set => _conductivityEvaluation = value;
        }

        public string LowerEvaluationLimit
        {
            get => _lowerEvaluationLimit;
            set => _lowerEvaluationLimit = value;
        }

        public string UpperEvaluationLimit
        {
            get => _upperEvaluationLimit;
            set => _upperEvaluationLimit = value;
        }

        public bool EvaluationBuzzer
        {
            get => _evaluationBuzzer;
            set => _evaluationBuzzer = value;
        }

        public bool EvaluationExternalOutput
        {
            get => _evaluationExternalOutput;
            set => _evaluationExternalOutput = value;
        }

        public string FG470
        {
            get => _fg470;
            set => _fg470 = value;
        }

        public bool Note
        {
            get => _note;
            set => _note = value;
        }
    }


    public class EmployeeModel
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmpCode { get; set; }
    }
}
