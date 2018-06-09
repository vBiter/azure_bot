using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace AzureBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        public enum FlowChoices
        {
            Chess,
            Search
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {


            var activity = await result as Activity;

            PromptDialog.Choice(context: context,
                                resume: DirectChoiceAsync,
                                options: (IEnumerable<FlowChoices>)Enum.GetValues(typeof(FlowChoices)),
                                prompt: "What do you want to do?",
                                retry: "Please choose again",
                                promptStyle: PromptStyle.Auto);


            // Calculate something for us to return
            //int length = (activity.Text ?? string.Empty).Length;

            //if (activity.Text.ToLower().Contains("hello"))
            //{
            //    await context.PostAsync($"Hello to you too!");

            //}
            //else
            //{
            //    await context.PostAsync($"You sent {activity.Text} which was {length} characters");
            //}

            // Return our reply to the user


            }

        public virtual async Task DirectChoiceAsync(IDialogContext context, IAwaitable<FlowChoices> activity)
        {
            FlowChoices response = await activity;

            if (response == FlowChoices.Chess)
            {
                await context.PostAsync("You chose the Chess Option");
                context.Done(this);

            }
            else if(response==FlowChoices.Search)
            {
                await context.PostAsync("You chose the Search Option");
                context.Done(this);
            }

        }
    }
}