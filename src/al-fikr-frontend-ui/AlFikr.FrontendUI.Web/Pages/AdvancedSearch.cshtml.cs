using AlFikr.FrontendUI.Entities;
using AlFikr.FrontendUI.Web.HttpClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlFikr.FrontendUI.Web.Pages
{
    public class AdvancedSearch : PageModel
    {
        private readonly IThesisServiceApiClient _thesisServiceApiClient;

        public AdvancedSearch(IThesisServiceApiClient thesisServiceApiClient)
        {
            _thesisServiceApiClient = thesisServiceApiClient;
        }

        [BindProperty]
        public AdvancedSearchEntity Criteria { get; set; } = new AdvancedSearchEntity { AdvancedSearchEntities = new List<AdvancedSearchItem> { new AdvancedSearchItem() } };

        public List<ThesisEntity> SearchResults { get; set; }

        public void OnGet()
        {
            // Ensure at least one search criterion exists
            if (Criteria?.AdvancedSearchEntities == null || !Criteria.AdvancedSearchEntities.Any())
            {
                Criteria.AdvancedSearchEntities.Add(new AdvancedSearchItem());
            }
        }
        public async Task<IActionResult> OnPostSearchAsync()
        {
            if (Criteria == null || !Criteria.AdvancedSearchEntities.Any())
            {
                ModelState.AddModelError(string.Empty, "At least one search criterion is required.");
                return Page();
            }

            var searchItems = Criteria.AdvancedSearchEntities.Select(c => new AdvancedSearchItem
            {
                Field = c.Field,
                Expression = c.Expression,
                Operator = c.Operator
            }).ToList();

            try
            {
                SearchResults = await _thesisServiceApiClient.AdvancedSearchThesesAsync(searchItems);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while performing the search: {ex.Message}");
                return Page();
            }

            if (SearchResults == null || !SearchResults.Any())
            {
                ModelState.AddModelError(string.Empty, "No results found.");
                return Page();
            }

            // Redirect to Publications page with selected IDs
            var ids = string.Join(",", SearchResults.Select(thesis => thesis.Id));

            return RedirectToPage("/Publications", new { ids });

            // return Redirect($"https://localhost:44311/Admin/Document/SearchResults/{ids}");

        }

    }
}
