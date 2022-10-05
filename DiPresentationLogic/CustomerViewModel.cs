namespace DiPresentationLogic
{
    public class CustomerViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsCompany { get; set; }

        public double TotalSales { get; set; }

        public bool IsSelected { get; set; }

        public CustomerEditorViewModel Edit()
        {
            var editor = new CustomerEditorViewModel(this.FirstName, this.LastName);
            editor.Email = this.Email;
            editor.IsCompany = this.IsCompany;
            editor.TotalSales = this.TotalSales;
            return editor;
        }
    }
}
