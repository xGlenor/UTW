using BaseLibrary.Contracts;
using BaseLibrary.Models;
using Microsoft.AspNetCore.Components;

namespace ClientUTW.Pages.Site;

public partial class Home : ComponentBase
{
    private int selectedIndex = 0;

    private IEnumerable<Session> sessions = new List<Session>();
    private IEnumerable<Lesson> lessonsBySession = new List<Lesson>();

    [Inject] 
    ILessonRepository LessonService { get; set; }
    [Inject]
    ISessionRepository SessionService { get; set; }
    
    protected async override Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        sessions = await SessionService.GetAll();
        
    }
    
    async void OnChange(int index)
    {
        lessonsBySession = await LessonService.GetBySessionId(index);
    }
    
}