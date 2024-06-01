using System.ComponentModel.DataAnnotations;

namespace BaseLibrary.enums;

public enum SessionType
{
    [Display(Description = "Summer")]
    SUMMER,
    [Display(Description = "Winter")]
    WINTER
}