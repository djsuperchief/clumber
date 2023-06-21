# clumber
![Logo](docs/logo.png)

UI Tests for humans

## Summary
Clumber is a wrapper around Microsoft Playwright (originally Selenium) to offer a UI testing framework driven by test cases written in understandable English. The basic idea is that the tests can be written by anyone, not just developers, and can therefore be maintained by any user within a team.

## Example Test Case
The following example is a test case being used to help develop the application
```
goto https://sadtrombone.com
click play
is pagetitle Sad Trombone
```

## Dev Notes
### Powershell - Raspberry Pi
[Raspbian Powershell install instructions](https://learn.microsoft.com/en-us/powershell/scripting/install/install-raspbian?view=powershell-7.3)

### Playwright Install
```
pwsh bin/Debug/netX/playwright.ps1 install
```