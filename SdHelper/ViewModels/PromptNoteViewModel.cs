using System.Collections.ObjectModel;
using System.Diagnostics;
using Prism.Mvvm;
using SdHelper.Models;

namespace SdHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PromptNoteViewModel : BindableBase
    {
        private ObservableCollection<ImageGenerationPrompt> imageGenerationPrompts = new ();
        private ImageGenerationPrompt selectedItem;

        public PromptNoteViewModel()
        {
            InjectDummies(); // デバッグモード時に限り実行される
        }

        public ObservableCollection<ImageGenerationPrompt> ImageGenerationPrompts
        {
            get => imageGenerationPrompts;
            private set => SetProperty(ref imageGenerationPrompts, value);
        }

        public ImageGenerationPrompt SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        [Conditional("DEBUG")]
        private void InjectDummies()
        {
            for (var i = 0; i < 30; i++)
            {
                var p = new ImageGenerationPrompt()
                {
                    Name = new TextWrapper() { Text = $"testName{i}", },
                    Prompt = new TextWrapper() { Text = $"best quality, test prompt, ((no {i})), (test), text", },
                    NegativePrompt = new TextWrapper() { Text = $"low quality, no {i}", },
                };

                ImageGenerationPrompts.Add(p);
            }

            SelectedItem = ImageGenerationPrompts[1];
        }
    }
}