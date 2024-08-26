using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AlFikr.FrontendUI.Web.Pages
{
    public class PublicationsModel : PageModel
    {
        private readonly IThesisServiceApiClient _thesisServiceApiClient;

        public PublicationsModel(IThesisServiceApiClient thesisServiceApiClient)
        {
            _thesisServiceApiClient = thesisServiceApiClient;
        }

        public List<ThesisEntity> Theses { get; set; } = new List<ThesisEntity>();
        public AuthorEntity Author { get; set; }
        public EditorEntity Editor { get; set; }
        public List<ThemeEntity> Themes { get; set; } = new List<ThemeEntity>();
        public string Ids { get; set; }

        public async Task OnGetAsync(string ids)
        {
            Ids = ids;

            if (!string.IsNullOrWhiteSpace(ids))
            {
                var idsList = ids.Split(',').Select(id => int.Parse(id)).ToList();

                foreach (var id in idsList)
                {
                    var thesis = await _thesisServiceApiClient.GetThesis(id);
                    if (thesis != null)
                    {
                        Theses.Add(thesis);
                        if (Author == null)
                        {
                            Author = await _thesisServiceApiClient.GetAuthorAsync(int.Parse(thesis.MainAuthorsIds[0]));
                        }
                        if (Editor == null)
                        {
                            Editor = await _thesisServiceApiClient.GetEditorAsync(thesis.IdEditor);
                        }

                        foreach (string themeId in thesis.ThemesIds)
                        {
                            var theme = await _thesisServiceApiClient.GetThemeAsync(int.Parse(themeId));
                            if (theme != null && !Themes.Contains(theme))
                            {
                                Themes.Add(theme);
                            }
                        }
                    }
                }
            }
        
    }
    }
}
