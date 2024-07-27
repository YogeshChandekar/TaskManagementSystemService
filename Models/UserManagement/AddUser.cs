namespace TaskManagementService.Models.UserManagement
{
    public class AddUser
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int IsAdmin { get; set; }
        public int Phone { get; set; }

        public int Approved { get; set; } = 0;

        public int Status { get; set; } = 1;

        public int Attemptcount { get; set; } = 0;
        public int Islocked { get; set; } = 1;
    }
}