using Microsoft.AspNetCore.Mvc;

namespace CustomerFeedbackSystem.Views.Shared.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
