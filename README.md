# StatusSwift <img src="StatusSwift/Resources/Images/logo.scale-100.png" alt="StatusSwift Logo" width="30" height="30" style="vertical-align: middle;"/>

<div align="center">

[![Download from Microsoft Store](https://img.shields.io/endpoint?url=https%3A%2F%2Fmicrosoft-store-badge.fly.dev%2Fapi%2Frating%3FstoreId%3D9nv7xnrpf579%26market%3DUS&style=for-the-badge&label=Download+from+Microsoft+Store&color=brightgreen&logo=windows11)](https://www.microsoft.com/store/productId/9nv7xnrpf579)
[![License: GPL-3.0](https://img.shields.io/badge/License-GPLv3-blue.svg?style=for-the-badge)](LICENSE.md)
[![Platform](https://img.shields.io/badge/Platform-Windows-blue?style=for-the-badge&logo=windows)](https://www.microsoft.com/windows)

</div>

StatusSwift is a lightweight tool that keeps your status in chat applications like Discord and Microsoft Teams set to "available." By simulating subtle mouse movements at random intervals, it prevents your status from automatically changing to "away" due to inactivity while you're reading, researching, or taking notes.

## Features

- **Automatic Status Management**: Keeps your status set to "available" in supported chat applications through unobtrusive mouse movements.
- **Smart Timing**: Uses random intervals between actions to appear more natural.
- **Multi-Platform Support**: Works with popular chat platforms such as Discord and Microsoft Teams.
- **User-Friendly**: Simple and intuitive interface for easy setup and use.
- **Runtime Timer**: Tracks how long StatusSwift has been active in your current session.
- **System Tray Integration**: Easily access StatusSwift from your system tray with status information.
- **Modern UI**: Clean, responsive design that adapts to both light and dark modes.
- **Open Source**: Free to use and modify under the GPL-3.0 license.

## Usage

- Open StatusSwift.
- Click the "Enable StatusSwift" button to activate.
- The timer will show how long StatusSwift has been running.
- Minimize or close the window to continue running in the background.
- Access StatusSwift anytime from the system tray icon.
- When finished, click "Disable StatusSwift" or exit from the tray menu.

## How It Works

StatusSwift works by simulating small mouse movements at random intervals between 30-180 seconds. These movements are subtle (just a few pixels) and designed to be unobtrusive while you work. The application runs efficiently in the background, with minimal resource usage, making it ideal for daily use without impacting system performance.

## System Requirements

- Windows 10/11
- .NET 10.0 or higher

## Privacy & Security

StatusSwift is designed with privacy in mind:
- No data collection or telemetry
- No internet connection required
- Open-source code available for security review

## Contributing

Contributions to StatusSwift are welcome! Whether it's bug reports, feature requests, or code contributions, your input helps make StatusSwift better for everyone.

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## Building From Source

StatusSwift is built using .NET MAUI. To build the project:

1. Install the latest Visual Studio with .NET MAUI support
2. Clone the repository
3. Open the solution in Visual Studio
4. Build and run the project

## License

StatusSwift is licensed under the GNU General Public License v3.0 (GPL-3.0). See the [LICENSE.md](LICENSE.md) file for details.

## Privacy Policy

For information about how StatusSwift handles your privacy, see the [Privacy Policy](PrivacyPolicy.md).

## Support

If you encounter issues or have questions about StatusSwift, please [open an issue](https://github.com/yourusername/StatusSwift/issues) on GitHub.

## Screenshots

<div align="center">
  <img src="docs/screenshots/StatusSwift - Screenshots@0,5x.png" alt="StatusSwift Screenshot" width="800"/>
</div>

## Acknowledgments

- [.NET MAUI](https://github.com/dotnet/maui) - The cross-platform framework used
- [SharpHook](https://github.com/TolikPylypchuk/SharpHook) - Used for simulating mouse movements
- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet) - MVVM framework
- [H.NotifyIcon](https://github.com/HavenDV/H.NotifyIcon) - System tray integration

---
