using System;
using System.Collections.Generic;

namespace Quizzie.DTOs;

public class CreateQuestionDto
{
    public string QuestionText { get; set; }
    //Navigation Properties
    public ICollection<CreateOrUpdateOptionDto> Options { get; set; }
}
