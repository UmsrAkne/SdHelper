using System;

namespace SdHelper.Models
{
    public class ImageGenerationPrompt
    {
        public ImageGenerationPrompt()
        {
        }

        public ImageGenerationPrompt(DateTime createdAt)
        {
            CreationDateTime = createdAt;
        }

        public TextWrapper Name { get; set; } = new () { Text = "PromptData", };

        public TextWrapper Prompt { get; set; } = new ();

        public TextWrapper NegativePrompt { get; set; } = new ();

        public TextWrapper Seed { get; set; } = new () { Text = "-1", };

        public DateTime CreationDateTime { get; set; }
    }
}