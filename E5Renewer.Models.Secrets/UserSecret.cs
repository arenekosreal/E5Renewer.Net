using System.Collections.Immutable;

namespace E5Renewer.Models.Secrets;

/// <summary>Readonly struct to store user secret and certificate passwords.</summary>
public readonly struct UserSecret
{
    /// <value>Users in the secret.</value>
    public ImmutableList<User> users { get; }

    /// <value>Passwords for user certificates in the secret.</value>
    public ImmutableDictionary<string, string>? passwords { get; }

    /// <value>If this is correct to be used.</value>
    public bool valid { get => this.users.All((user) => user.valid); }

    /// <summary>Initialize <see cref="UserSecret"/> instance with arguments given.</summary>
    public UserSecret(ImmutableList<User> users, ImmutableDictionary<string, string>? passwords) =>
        (this.users, this.passwords) = (users, passwords);
}
