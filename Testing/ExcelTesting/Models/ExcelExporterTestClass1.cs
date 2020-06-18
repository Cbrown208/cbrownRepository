using System;

namespace Pas.ExcelExporterTests.Models
{
    public class ExcelExporterTestClass1
    {
        public int Id { get; set; }
        public Guid FacilityId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Notes { get; set; }
        public DateTime LastTouchedDate { get; set; }
    }
}
