namespace Bookist.Core.Dtos
{
    //public class TagRequestDto
    //{
    //    public string Name { get; set; }
    //}

    public class TagResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class TagDetailResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int BookCount { get; set; }
    }
}
