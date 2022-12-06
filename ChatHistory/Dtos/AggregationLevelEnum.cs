using System.ComponentModel.DataAnnotations;

namespace ChatHistory.Api.Dtos
{
    public enum AggregationLevelEnum
    {
        [Display(Name = "Continuously")]
        Continuously,
        [Display(Name = "Hourly")]
        Hourly,
        [Display(Name = "Daily")]
        Daily
    }
}
