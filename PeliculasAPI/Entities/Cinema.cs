namespace PeliculasAPI.Entities
{
    public class Cinema: IModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}