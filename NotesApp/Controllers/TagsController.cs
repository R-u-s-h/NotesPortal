using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Controllers.CustomAuthorizeAttributes;
using NotesApp.Controllers.CustomNotesAttributes;
using NotesApp.DbStuff.Models.Notes;
using NotesApp.DbStuff.Repositories.Interfaces.Notes;
using NotesApp.Enum;
using NotesApp.Models.Notes;

namespace NotesApp.Controllers;

[ApiGroup("Notes")]
[Authorize]
public class TagsController : Controller
{
    private ITagRepository _tagRepository;

    public TagsController(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public IActionResult Index()
    {
        var tagViewModels = _tagRepository
            .GetAll()
            .Select(x => new TagViewModel
            {
                Name = x.Name,
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
}