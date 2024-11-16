# MS365 E5 Subscription Renewer

[![Run tests](https://github.com/arenekosreal/E5Renewer.Net/actions/workflows/test.yaml/badge.svg)](https://github.com/arenekosreal/E5Renewer.Net/actions/workflows/test.yaml)

## What is this

A tool to renew e5 subscription by calling msgraph APIs

## How to use
1. Create Application

    See [Register Application](https://learn.microsoft.com/graph/auth-register-app-v2) and [Configure Permissions](https://learn.microsoft.com/graph/auth-v2-service#2-configure-permissions-for-microsoft-graph) for more info. We need `tenant id`, `client id` and `client secret` of your application to access msgraph APIs.

    <details>
    <summary>Tips for people who like certificate instead secret:</summary>

    - If you add certificate after created application and added secret, the `client_id` may be changed so please update it.
    - Using pfx format to this tool is tested. But you only need to upload public key part(*.crt) to Azure.
    - If your certificate has a password, you can create a `passwords` key in config like this:

      ```json
      {
          "passwords": {
              "<sha512sum>": "<password>"
          }
      }
      ```

      `<sha512sum>` is the sha512 sum of the certificate file in lower case and `<password>` is its password in **plain**, please keep the configuration in secret to avoid someone using your certificate without being permitted.
    </details>
2. Create Configuration

    Copy [`config.json.example`](./config.json.example) to `config.json`, edit it as your need. You can always add more credentials. Please edit `auth_token` so only people you authenticated can access the statistics.

    We will listen on tcp socket by default, if `listen_addr` is an empty string and your platform supports Unix domain socket, we will listen on Unix domain socket with path `listen_socket` and permission `listen_socket_permission`.

    If you want to use certificate instead secret, which is better for security, you can write a `certificate` key with path to your certificate file instead `secret` key.

    Tips: We support json, yaml and toml formats, just let their contents be equal, the configuration result is same.
    
    > [!NOTE]
    > Due to that C# does not have a native octal number support, we use the `listen_socket_permission` as unix permission directly.
    > For example, if you are using json format, you need to set it to `438` in order to see socket mode is `rw-rw-rw-`.

3. Install .NET

    See [here](https://learn.microsoft.com/en-us/dotnet/core/install/) for more info, we need .NET 8 and later.

4. Get program

    You can download prebuilt program at [Release](https://github.com/arenekosreal/E5Renewer.Net/releases) page, simply choose which one you want, download it, unpack it and run it.
    If you want to build from source, you can run `dotnet publish` and you will find binaries at `bin/Release/net8.0`.

5. Run program

    Simply run `./E5Renewer` in binaries folder.

## Get running statistics

Using `curl` or any tool which can send http request, send request to `http://<listen_addr>:<listen_port>` or unix socket `<listen_socket>`,
each request should be sent with header `Authentication: <auth_token>`.
You will get json response if everything is fine. If it is a GET request, send milisecond timestamp in query param `timestamp`,
If it is a POST request, send milisecond timestamp in post json with key `timestamp` and convert it to string.

For example:

<details>

<summary>HTTP API for /v1/list_apis</summary>

```
curl -H 'Authentication: <auth_token>' -H 'Accept: application/json' \
    'http://<listen_addr>:<listen_port>/v1/list_apis?timestamp=<timestamp>' | jq '.'
{
    "method": "list_apis",
    "args": {},
    "result": [
        "AgreementAcceptances.Get",
        "Admin.Get",
        "Agreements.Get",
        "AppCatalogs.Get",
        "ApplicationTemplates.Get",
        "Applications.Get",
        "AuditLogs.Get",
        "AuthenticationMethodConfigurations.Get",
        "AuthenticationMethodsPolicy.Get",
        "CertificateBasedAuthConfiguration.Get",
        "Chats.Get", "Communications.Get",
        "Compliance.Get",
        "Connections.Get",
        "Contacts.Get",
        "DataPolicyOperations.Get",
        "DeviceAppManagement.Get",
        "DeviceManagement.Get",
        "Devices.Get",
        "Direcory.Get",
        "DirectoryObjects.Get",
        "DirectoryRoleTemplates.Get",
        "DirectoryRoles.Get",
        "DomainDnsRecords.Get",
        "Domains.Get",
        "Drives.Get",
        "Education.Get",
        "EmployeeExperience.Get",
        "External.Get",
        "FilterOperators.Get",
        "Functions.Get",
        "GroupLifecyclePolicies.Get",
        "GroupSettingTemplates.Get",
        "GroupSetings.Get",
        "Groups.Get",
        "Identity.Get",
        "IdentityGovernance.Get",
        "IdentityProtection.Get",
        "IdentityProviders.Get",
        "InfomationProtecion.Get",
        "Invitations.Get",
        "OAuth2PermissionGrants.Get",
        "Organization.Get",
        "PermissionGrants.Get",
        "Places.Count.Get",
        "Places.GraphRoom.Get",
        "Planner.Get",
        "Policies.Get",
        "Print.Get",
        "Privacy.Get",
        "Reports.Get",
        "RoleManagement.Get",
        "SchemaExtensions.Get",
        "ScopedRoleMemberships.Get",
        "Search.Get",
        "Security.Get",
        "ServicePrincipals.Get",
        "Shares.Get",
        "Sites.Get",
        "Solutions.Get",
        "SubscribedSkus.Get",
        "Subscriptions.Get",
        "Teams.Get",
        "TeamsTemplates.Get",
        "Teamwork.Get",
        "TenantRelationships.Get",
        "Users.Get"
    ],
    "timestamp": "<timestamp_returned_by_server>"
}

```
</details>

Server will only accept request less than **30 seconds** older than server time.

See [http-api.md](./http-api.md) for possible apis

**Note:** This program supports **HTTP** only, and it is insecure. Please use a reverse proxy tool like `nginx` or `apache` in front of it to transfer data through untrusted environment.
