using pawm.Services.Interfaces;

namespace pawm.Services
{
    public class MauiMessageService : IMessageDisplayService
    {
        public async Task DisplayMessage(string message)
        {
            await Shell.Current.DisplayAlert("Message", message, "ok");
        }

        public async Task<string> AskForText(string title, string message, int length = 100, string defaultText = "")
        {
            var result = await Shell.Current.DisplayPromptAsync(title, message, keyboard: Keyboard.Text, initialValue: defaultText);
            return result;
        }
    }
}
