# 🧠 Memory Game (WPF + C#)

A fully-featured memory matching game built with WPF and C#. This project demonstrates the use of **MVVM architecture** and **data binding** in a desktop application, while offering a complete gaming experience with user management, saving/loading games, customizable board size, and player statistics.

Inspired by: [https://www.webgamesonline.com/memory/](https://www.webgamesonline.com/memory/)

---

## 🎮 Features

- 🔐 **User Sign-In system** with the ability to:
  - Create new users with a profile image
  - Delete users and all associated data
  - Select a user before playing
    
    ![image](https://github.com/user-attachments/assets/08264f40-1d97-44ac-a972-d5695ea06de1)

- 🧩 **Memory game** with:
  - Standard (4x4) or Custom board size (2–6 range, even number of cards)
  - Multiple image categories (3 predefined with cats, flowers and shells)
  - Card flipping and match logic
  - Timer and win/loss condition (the user can choose the time)
  - Randomized layout for every new game
 
    ![image](https://github.com/user-attachments/assets/33065b96-f894-425b-83b7-e68b6124f1b4)

    ![image](https://github.com/user-attachments/assets/f5ba1c41-1e8c-43b8-8740-6c5e34cd889e)


- 💾 **Save & Load Game** state (category, board, timer, matched pairs)
- 📊 **Statistics tracking** per user (games played, games won)

    ![image](https://github.com/user-attachments/assets/3a05a0b1-b9c6-490d-9aa9-dad94212e145)


- 🧹 **Full user deletion** (profile image, save files, stats)
- 🖼️ **About dialog** with student details
- 👨‍💻 Designed using **MVVM** and **WPF Data Binding**

---
🧪 **Technologies Used**
C#

.NET Framework / .NET Core

WPF (Windows Presentation Foundation)

MVVM Pattern

JSON/XML file serialization

ICommand for button actions
