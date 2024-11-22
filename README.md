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

    Copy [`config.json.example`](./config.json.example) to `config.json`, edit it as your need. You can always add more credentials.

    If you want to use certificate instead secret, which is better for security, you can write a `certificate` key with path to your certificate file instead `secret` key.
    If we find you set `certificate`, it will always be used instead `secret`.

    Setting days is needed to be cautious, as it means `DayOfWeek` in program, 
    check [here](https://learn.microsoft.com/en-us/dotnet/api/system.dayofweek#fields) to find out its correct value.
    
    Tips: We support json, yaml and toml formats, just let their contents be equal, the configuration result is same.

3. Install .NET

    See [here](https://learn.microsoft.com/en-us/dotnet/core/install/) for more info, we need .NET 8 and later.

4. Get program

    You can download prebuilt program at [Release](https://github.com/arenekosreal/E5Renewer.Net/releases) page, simply choose which one you want, download it, unpack it and run it.
    If you want to build from source, you can run `dotnet publish` and you will find binaries at `bin/Release/net8.0`.

5. Run program

    Simply run `./E5Renewer` in binaries folder with arguments needed.
    
    Here are supported arguments:
    
    - `--systemd`: If runs in systemd environment, most of the time you should not need it.
    - `--user-secret`: The path to the user secret file. User secret file is used to storage sensitive information, so please keep it safe.
    - `--token`: The string to access json api, please keep it safe.
    - `--token-file`: The file which contains token, please keep that file safe.
    - `--listen-tcp-socket`: The tcp socket to listen instead default one(`127.0.0.1:5000`).
    - `--listen-unix-socket-path`: The path to create a unix domain socket to access json api.
    - `--listen-inux-socket-permission`: The permission to the unix domain socket file.
    - All AspNet.Core supported arguments. See [here](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/#command-line) for more info.
    
    We will listen on tcp socket `127.0.0.1:5000` by default, if you want to customize it, 
    you need to set commandline argument `--listen-tcp-socket` like `--listen-tcp-socket=127.0.0.1:8888`.
    You can also choose listen unix domain socket by setting commandline argument like `--listen-unix-socket-path=/path/to/socket` 
    and set socket file permission with argument like `--listen-unix-socket-permission=511`.
    
> [!NOTE]
> If `--token` and `--token-file` both are specified, we prefer `--token`. If you forget to set neither of them, we use a randomly generated value.
> You can find it out in log output after sending any request to the program and meeting an authentication error.
    
> [!IMPORTANT]
> If you want to set unix socket permission, you have to write its actual value instead octal format. For example, using `511` instead `777` is required.

## Get running statistics

Using `curl` or any tool which can send http request, send request to `http://<listen_addr>:<listen_port>` or unix socket `<listen_socket>`,
each request should be sent with header `Authorization: Bearer <auth_token>`.
You will get json response if everything is fine. If it is a GET request, send milisecond timestamp in query param `timestamp`,
If it is a POST request, send milisecond timestamp in post json with key `timestamp` and convert it to string.
Most of the time, we will return json instead plain text, but you need to check response code to see if request is success.

For example:

<details>

<summary>HTTP API for /v1/list_apis</summary>

```
curl -H 'Authorization: Bearer <auth_token>' -H 'Accept: application/json' \
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
