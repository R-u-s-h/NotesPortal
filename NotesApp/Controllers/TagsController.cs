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
public class TagsController : Controller
{
    private ITagRepository _tagRepository;
    private IAuthNotesService _authNotesService;

    public TagsController(ITagRepository tagRepository, IAuthNotesService authNotesService)
    {
        _tagRepository = tagRepository;
        _authNotesService = authNotesService;
    }
    public IActionResult Index()
    {
        var canDelete = _authNotesService.IsAuthenticated() &&
                        (_authNotesService.GetRole() == NotesUserRole.Administrator ||
                         _authNotesService.GetRole() == NotesUserRole.Moderator);

        var tagViewModels = _tagRepository
            .GetAll()
            .Select(x => new TagViewModel
            {
                Id = x.Id,
                Name = x.Name,
                IsAllowedToDelete = canDelete
            })
            .ToList();

        return View(tagViewModels);
    }

    // /Tags/Add (GET)
    [HttpGet]
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator, NotesUserRole.User)]
    public IActionResult Add()
    {
        return View();
    }

    // /Tags/Add (POST)
    [HttpPost]
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator, NotesUserRole.User)]
    public IActionResult Add(TagViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var tag = new Tag
        {
            Name = viewModel.Name,
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow
        };

        _tagRepository.Add(tag);

        return RedirectToAction("Index", "Notes");
    }

    [HttpGet]
    [RoleNotes(NotesUserRole.Administrator, NotesUserRole.Moderator)]
    public IActionResult Remove(int id)
    {
        _tagRepository.Remove(id);

        return RedirectToAction("Index");
    }
}