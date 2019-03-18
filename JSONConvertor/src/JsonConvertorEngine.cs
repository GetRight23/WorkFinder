namespace JSONConvertor
{
    public class JsonConvertorEngine
    {
        public AddressJsonConvertor AddressJsonConvertor { get; private set; }
        public CityDistrictJsonConvertor CityDistrictJsonConvertor { get; private set; }
        public CityJsonConvertor CityJsonConvertor { get; private set; }
        public FeedbackConvertor FeedbackConvertor { get; private set; }
        public OrderConvertor OrderConvertor { get; private set; }
        public OrdersListConvertor OrdersListConvertor { get; private set; }
        public OrderToServiceConvertor OrderToServiceConvertor { get; private set; }
        public PhotoConvertor PhotoConvertor { get; private set; }
        public ProfessionCategoryConvertor ProfessionCategoryConvertor { get; private set; }
        public ProfessionConvertor ProfessionConvertor { get; private set; }
        public ProfessionToWorkerConvertor ProfessionToWorkerConvertor { get; private set; }
        public ServiceConvertor ServiceConvertor { get; private set; }
        public UserConvertor UserConvertor { get; private set; }
        public WorkerConvertor WorkerConvertor { get; private set; }

        public JsonConvertorEngine()
        {
            AddressJsonConvertor = new AddressJsonConvertor();
            CityDistrictJsonConvertor = new CityDistrictJsonConvertor();
            CityJsonConvertor = new CityJsonConvertor();
            FeedbackConvertor = new FeedbackConvertor();
            OrderConvertor = new OrderConvertor();
            OrdersListConvertor = new OrdersListConvertor();
            OrderToServiceConvertor = new OrderToServiceConvertor();
            PhotoConvertor = new PhotoConvertor();
            ProfessionCategoryConvertor = new ProfessionCategoryConvertor();
            ProfessionConvertor = new ProfessionConvertor();
            ProfessionToWorkerConvertor = new ProfessionToWorkerConvertor();
            ServiceConvertor = new ServiceConvertor();
            UserConvertor = new UserConvertor();
            WorkerConvertor = new WorkerConvertor();
        }
    }
}
