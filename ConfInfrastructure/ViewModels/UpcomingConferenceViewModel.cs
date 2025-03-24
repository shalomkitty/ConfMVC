namespace ConfInfrastructure.ViewModels
{
    public class UpcomingConferenceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
    }
}
