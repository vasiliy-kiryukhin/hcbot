using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups
{
    /// <summary>
    /// Represents a custom keyboard with reply options
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ReplyKeyboardMarkup : ReplyMarkupBase
    {
        /// <summary>
        /// Array of button rows, each represented by an Array of KeyboardButton objects
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public IEnumerable<IEnumerable<KeyboardButton>> Keyboard { get; set; }

        /// <summary>
        /// Optional. Requests clients to resize the keyboard vertically for optimal fit (e.g., make the keyboard smaller if there are just two rows of <see cref="KeyboardButton"/>). Defaults to <c>false</c>, in which case the custom keyboard is always of the same height as the app's standard keyboard.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool ResizeKeyboard { get; set; }

        /// <summary>
        /// Optional. Requests clients to hide the keyboard as soon as it's been used. Defaults to <c>false</c>.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool OneTimeKeyboard { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="ReplyKeyboardMarkup"/>
        /// </summary>
        public ReplyKeyboardMarkup()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ReplyKeyboardMarkup"/> with one button
        /// </summary>
        /// <param name="button">Button on keyboard</param>
        public ReplyKeyboardMarkup(KeyboardButton button)
            : this(new[] { button })
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ReplyKeyboardMarkup"/>
        /// </summary>
        /// <param name="keyboardRow">The keyboard row.</param>
        /// <param name="resizeKeyboard">if set to <c>true</c> the keyboard resizes vertically for optimal fit.</param>
        /// <param name="oneTimeKeyboard">if set to <c>true</c> the client hides the keyboard as soon as it's been used.</param>
        public ReplyKeyboardMarkup(IEnumerable<KeyboardButton> keyboardRow, bool resizeKeyboard = default(bool),
            bool oneTimeKeyboard = default(bool))
            : this(new[] { keyboardRow }, resizeKeyboard, oneTimeKeyboard)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplyKeyboardMarkup"/> class.
        /// </summary>
        /// <param name="keyboard">The keyboard.</param>
        /// <param name="resizeKeyboard">if set to <c>true</c> the keyboard resizes vertically for optimal fit.</param>
        /// <param name="oneTimeKeyboard">if set to <c>true</c> the client hides the keyboard as soon as it's been used.</param>
        public ReplyKeyboardMarkup(IEnumerable<IEnumerable<KeyboardButton>> keyboard, bool resizeKeyboard = default(bool),
            bool oneTimeKeyboard = default(bool))
        {
            Keyboard = keyboard;
            ResizeKeyboard = resizeKeyboard;
            OneTimeKeyboard = oneTimeKeyboard;
        }

        /// <summary>
        /// Generates a reply keyboard markup with one button
        /// </summary>
        /// <param name="text">Button's text</param>
        public static implicit operator ReplyKeyboardMarkup(string text) =>
            text == null
                ? default(ReplyKeyboardMarkup)
                : new ReplyKeyboardMarkup(new[] { new KeyboardButton(text) });

        /// <summary>
        /// Generates a reply keyboard markup with multiple buttons on one row
        /// </summary>
        /// <param name="texts">Texts of buttons</param>
        public static implicit operator ReplyKeyboardMarkup(string[] texts) =>
            texts == null
                ? null
                : new[] { texts };

        /// <summary>
        /// Generates a reply keyboard markup with multiple buttons
        /// </summary>
        /// <param name="textsItems">Texts of buttons</param>
        public static implicit operator ReplyKeyboardMarkup(string[][] textsItems) =>
            textsItems == null
                ? default(ReplyKeyboardMarkup)
                : new ReplyKeyboardMarkup(
                    textsItems.Select(texts =>
                        texts.Select(t => new KeyboardButton(t))
                    ));
    }
}
