namespace SWP391.OnlineShop.ServiceModel.ViewModels.Profiles
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
        public bool IsDefault { get; set; }
    }
}
