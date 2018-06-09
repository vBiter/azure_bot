using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace AzureBot.Dialogs
{
    [Serializable]
    public class HeroCardDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {

            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public enum TaskChoiceList
        {
            Chess,
            Search
        }

        private async Task MessageReceivedAsync(IDialogContext context,IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var WelcomeMessage = context.MakeMessage();

            //PromptDialog.Choice(context: context,resume: ChoiceReceivedAsync, opt ;

            WelcomeMessage.Text = "Get ready for Chess";

            await context.PostAsync(WelcomeMessage);

            await this.DisplayHeroCard(context);
        }

        public async Task DisplayHeroCard(IDialogContext context)
        {
            var replyMessage = context.MakeMessage();
            Attachment attachment = GetExampleHeroCard();
            replyMessage.Attachments = new List<Attachment> { attachment };
            await context.PostAsync(replyMessage);
        }

        public virtual async Task ChoiceReceivedAsync(IDialog context,IAwaitable<TaskChoiceList> activity)
        {
            TaskChoiceList response = await activity;


        }

        private static Attachment GetExampleHeroCard()
        {

            var herocard = new HeroCard
            {
                Title = "Chess",
                Subtitle = "Learn more about chess",
                Tap = new CardAction(ActionTypes.OpenUrl, "Learn more", "https://www.chess.com/play"),
                Images = new List<CardImage> { new CardImage("https://betacssjs.chesscomfiles.com/bundles/web/images/web/play.png") }
            };

            return herocard.ToAttachment();
        }
    }

}