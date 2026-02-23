namespace HrApp.Services.Results
{
    public class BaseResult
    {
        private List<string> _errors = new List<string>();
        private bool _succeeded;

        public BaseResult()
        {
            _succeeded = true;
        }

        public IEnumerable<string> Errors => _errors;
        public string ErrorString => string.Join(',', Errors);
        public bool Succeeded => _succeeded;

        public void Failed(string errors)
        { 
            _errors.Add(errors);
            _succeeded = false;
        }
    }
}
