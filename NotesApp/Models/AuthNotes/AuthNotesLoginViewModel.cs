using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NotesApp.Models.CustomValidationAttributtes.Notes;

namespace NotesApp.Models.AuthNotes;

public class AuthNotesLoginViewModel
{
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [ValidateNever] public string? ReturnUrl { get; set; }
}