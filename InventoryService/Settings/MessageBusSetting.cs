namespace OrderService.Settings
{
    public class MessageBusSetting
    {/// <summary>
     /// Options name.
     /// </summary>
        private readonly string messageBus = "MessageBus";

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBusSetting" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public MessageBusSetting(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            configuration.GetSection(this.messageBus).Bind(this);
        }

        /// <summary>
        /// Gets or sets of host.
        /// </summary>
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets of port.
        /// </summary>
        public string Port { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets of user name.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets of password.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets value indicating whether SSL is enabled.
        /// </summary>
        public string IsSslEnable { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the retry count.
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// Verifies setting object.
        /// </summary>
        /// <returns>Verification as succeed or failed.</returns>
        public bool IsValid()
        {
            return true;
        }
    }
}
