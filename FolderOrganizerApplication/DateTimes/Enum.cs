using System.ComponentModel.DataAnnotations;

namespace FolderOrganizerApplication.DateTimes
{
    public enum PersianDay
    {
        [Display(Name = "شنبه")]
        Shanbe = 0,
        [Display(Name = "یکشنبه")]
        YekShanbe = 1,
        [Display(Name = "دوشنبه")]
        DoShanbe = 2,
        [Display(Name = "سه شنبه")]
        SeShanbe = 3,
        [Display(Name = "چهارشنبه")]
        CharShanbe = 4,
        [Display(Name = "پنج شنبه")]
        PanjShanbe = 5,
        [Display(Name = "جمعه")]
        Jome = 6
    }
}