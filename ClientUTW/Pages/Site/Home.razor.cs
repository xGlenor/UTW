using BaseLibrary.Contracts;
using BaseLibrary.Models;
using Microsoft.AspNetCore.Components;

namespace ClientUTW.Pages.Site;

public partial class Home : ComponentBase
{
    private int selectedIndex = 0;

    private IEnumerable<Lesson> lessonsBySession = new List<Lesson>();

    [Inject] ILessonRepository LessonService { get; set; }
    [Inject] ISessionRepository SessionService { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        selectedIndex = SessionService.GetCurrentSemester(DateTime.Now).Id;

        lessonsBySession = await LessonService.GetBySessionId(selectedIndex);
    }
}