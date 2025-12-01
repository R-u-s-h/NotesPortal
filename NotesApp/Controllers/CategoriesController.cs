using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Controllers.CustomAuthorizeAttributes;
using NotesApp.Controllers.CustomNotesAttributes;
using NotesApp.DbStuff.Models.Notes;
using NotesApp.DbStuff.Repositories.Interfaces.Notes;
using NotesApp.Enum;
using NotesApp.Models.Notes;
using NotesApp.Services;

namespace NotesApp.Controllers;

[ApiGroup("Notes")]
[Authorize]
public class CategoriesController : Controller
{
    private ICategoryRepository _categoryRepository;
    private IAuthNotesService _authNotesService;

    public CategoriesController(ICategoryRepository categoryRepository, IAuthNotesService authNotesService)
    {
        _categoryRepository = categoryRepository;
        _authNotesService = authNotesService;
    }
    public IActionResult Index()
    {
        var canDelete = _authNotesService.IsAuthenticated() &&
                        (_authNotesService.GetRole() == NotesUserRole.Administrator ||
                         _authNotesService.GetRole() == NotesUserRole.Moderator);

        var categoryViewModels = _categoryRepository
            .GetAll()
            .Select(x => new CategoryViewModel
            {
                Id = x.Id,
                Name = x.Name,
                IsAllowedToDelete = canDelete
            })
            .ToList();

        return View(categoryViewModels);
    }
    
    // /Categories/Add (GET)
    [HttpGet]
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator, NotesUserRole.User)]
    public IActionResult Add()
    {
        return View();
    }

    // /Categories/Add (POST)
    [HttpPost]
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator, NotesUserRole.User)]
    public IActionResult Add(CategoryViewModel viewModel)
    {
        
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var category = new Category
        {
            Name = viewModel.Name,
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow
        };

        _categoryRepository.Add(category);

        return RedirectToAction("Index", "Notes");
    }

    [HttpGet]
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator)]
    public IActionResult Remove(int id)
    {
        _categoryRepository.Remove(id);

        return RedirectToAction("Index");
    }
}