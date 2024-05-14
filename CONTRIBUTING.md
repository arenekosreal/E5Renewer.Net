# Contribution guide for E5Renewer

Hello, very glad to know that you are going to make contributions to this project! But we have some code quality requirements. Don't worry, we will list them below.

Before you want to change the repository, please make a fork, everything listed below is done in your fork.

We think you have already read [README.md](README.md) about how to setup poetry and python environment.
But if we are wrong, please feel free to go there to setup dotnet environment.
Here are steps to prepare a development environment:

Run `dotnet restore` in the repository to install them.

Yes, just one step! So easy, doesn't it? Then you can do changes to this project as you like, such as fixing bugs, adding features, etc.
Please do not forget to add some tests for your changes, this is optional but helps preventing bugs at development stage.

**NOTE**: Please follow our name style.
In a world, you need using `camelCase` for variables, using `PascalCase` for classes and Functions, using `kebab-case` for commandline arguments, and the last one, using `snake_case` for http apis and configurations.

After you finish your masterpiece, don't hurry to commit and push directly, please run `dotnet test` to make sure all tests are passed.
We have set running ruff to check code format and quality, so if theres something wrong raised by ruff, please follow outputs to fix it.

After no error is raised, you can `git add` and `git commit` your changes.
We have no many rules/limits on commit message, just one request: let us know what you are doing in the commit.
For example, you can write like this in commit message:
```
Add xxx feature
```
or with some extra description:
```
Add xxx feature

This helps xxx
```

Finally, you can push changes to your repository and create a pull request so we can merge it after everything is checked.
