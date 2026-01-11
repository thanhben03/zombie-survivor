# 🧟 Zombie Survival Shooter - Unity 3D Game

[![Unity Version](https://img.shields.io/badge/Unity-2021.3.45f1-blue.svg)](https://unity.com/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)
[![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)]()

Một game bắn súng sinh tồn zombie 3D được phát triển bằng Unity Engine. Người chơi sẽ phải chiến đấu với hordes zombie, hoàn thành các nhiệm vụ và sống sót qua nhiều stage khác nhau.

## 📋 Mục Lục

- [Tổng Quan](#tổng-quan)
- [Tính Năng Chính](#tính-năng-chính)
- [Công Nghệ Sử Dụng](#công-nghệ-sử-dụng)
- [Cấu Trúc Project](#cấu-trúc-project)
- [Cài Đặt](#cài-đặt)
- [Hướng Dẫn Chơi](#hướng-dẫn-chơi)
- [Kiến Trúc Code](#kiến-trúc-code)
- [Tính Năng Kỹ Thuật](#tính-năng-kỹ-thuật)
- [Tác Giả](#tác-giả)

## 🎮 Tổng Quan

**Zombie Survival Shooter** là một game hành động 3D với gameplay tập trung vào việc sinh tồn và chiến đấu với zombie. Game được thiết kế với hệ thống stage-based, mỗi stage có độ khó tăng dần và yêu cầu người chơi hoàn thành các nhiệm vụ cụ thể để tiến bộ.

### Điểm Nổi Bật

- ✅ Hệ thống AI zombie thông minh với NavMesh
- ✅ Nhiều loại vũ khí với hệ thống reload và ammo
- ✅ Hệ thống nhiệm vụ (Objectives) đa dạng
- ✅ Hệ thống stage với spawner động
- ✅ Điều khiển phương tiện (Vehicle)
- ✅ UI/UX hoàn chỉnh với health bar, damage popup
- ✅ Object Pooling để tối ưu performance
- ✅ Animation system mượt mà

## ✨ Tính Năng Chính

### 🎯 Hệ Thống Gameplay

- **Combat System**: Bắn súng với nhiều loại vũ khí, hệ thống reload, ammo management
- **Enemy AI**: Zombie có 3 trạng thái: Patrol → Chase → Attack với NavMesh pathfinding
- **Objective System**: Hệ thống nhiệm vụ động với tracking và completion
- **Stage System**: Nhiều stage với độ khó tăng dần, spawner tự động điều chỉnh
- **Vehicle System**: Điều khiển xe để di chuyển và tấn công zombie

### 👤 Hệ Thống Player

- **Movement**: Di chuyển mượt mà với walk, run, jump
- **Health System**: Hệ thống máu với visual feedback khi bị tấn công
- **Weapon Management**: Nhặt và sử dụng nhiều loại vũ khí
- **Character Selection**: Chọn nhân vật trước khi bắt đầu game
- **Camera System**: Hệ thống camera linh hoạt (Third-person, Aim)

### 🧟 Hệ Thống Zombie

- **AI Behavior**: 
  - Patrol: Tuần tra giữa các điểm cố định
  - Chase: Đuổi theo player khi phát hiện
  - Attack: Tấn công khi vào tầm
- **Health & Damage**: Hệ thống máu riêng với death animation
- **Spawn System**: Spawn động dựa trên stage configuration

### 🎨 Hệ Thống UI/UX

- **Main Menu**: Menu chính với character selection
- **In-Game UI**: Health bar, ammo count, damage popup, stage progress
- **Game Over/Win Menu**: Menu kết thúc game
- **Objective Tracker**: Hiển thị và theo dõi tiến độ nhiệm vụ

## 🛠️ Công Nghệ Sử Dụng

- **Engine**: Unity 2021.3.45f1 (LTS)
- **Language**: C#
- **Rendering**: 3D Graphics với Post-Processing
- **AI**: Unity NavMesh System
- **UI**: Unity UI (uGUI) + TextMesh Pro
- **Audio**: Unity Audio System
- **Animation**: Unity Animator Controller
- **Physics**: Unity Physics System với Character Controller

### Assets & Packages

- **TextMesh Pro**: Text rendering system
- **JMO Assets WarFX**: Particle effects và visual effects
- **Low Poly Guns**: Weapon models và animations
- **Flooded Grounds**: Environment assets

## 📁 Cấu Trúc Project

```
Assets/
├── Scripts/
│   ├── Manager/              # Core game managers
│   │   ├── GameManager.cs    # Quản lý game state
│   │   ├── StageManager.cs   # Quản lý stage progression
│   │   ├── UIManager.cs      # Quản lý UI elements
│   │   └── PoolManager.cs    # Object pooling system
│   ├── Player/               # Player scripts
│   │   ├── PlayerScript.cs   # Main player controller
│   │   ├── PlayerHealthBar.cs
│   │   └── CharacterSelect.cs
│   ├── Zombie/               # Zombie AI scripts
│   │   ├── Zombie1.cs        # Zombie AI controller
│   │   ├── Zombie2.cs
│   │   └── ZombieSpawner.cs
│   ├── Weapon/               # Weapon system
│   │   ├── Weapon.cs         # Base weapon class
│   │   └── Rifle.cs          # Rifle implementation
│   ├── Objective/            # Objective system
│   │   ├── ObjectiveManager.cs
│   │   └── Objective*.cs
│   ├── Scriptable/           # ScriptableObjects
│   │   ├── Weapon/WeaponData.cs
│   │   └── Stage/StageData.cs
│   └── ...
├── Scenes/
│   ├── MainMenu.unity        # Main menu scene
│   └── ZombieLand.unity      # Main game scene
├── Prefabs/                  # Game prefabs
├── Audio/                    # Sound effects & music
├── Animations/               # Animation files
└── ...
```

## 🚀 Cài Đặt

### Yêu Cầu Hệ Thống

- **Unity Version**: 2021.3.45f1 hoặc tương thích
- **OS**: Windows 10/11
- **RAM**: Tối thiểu 4GB
- **Graphics**: DirectX 11 compatible

### Hướng Dẫn Cài Đặt

1. **Clone repository**
   ```bash
   git clone https://github.com/yourusername/zombie-survivor.git
   cd zombie-survivor
   ```

2. **Mở project trong Unity**
   - Mở Unity Hub
   - Click "Add" và chọn thư mục project
   - Đảm bảo Unity version 2021.3.45f1 được cài đặt
   - Click "Open" để mở project

3. **Import dependencies** (nếu cần)
   - Unity sẽ tự động import các packages cần thiết
   - Đợi Unity compile scripts

4. **Chạy game**
   - Mở scene `Assets/Scenes/MainMenu.unity`
   - Click Play button trong Unity Editor

### Build Game

1. File → Build Settings
2. Chọn platform (Windows)
3. Click "Build" và chọn thư mục output
4. Chạy file `.exe` được tạo

## 🎮 Hướng Dẫn Chơi

### Điều Khiển

| Phím | Hành Động |
|------|-----------|
| `W/A/S/D` | Di chuyển |
| `Left Shift` | Chạy nhanh |
| `Space` | Nhảy |
| `Mouse Left Click` | Bắn |
| `R` | Nạp đạn |
| `F` | Lên xe |
| `G` | Xuống xe |
| `ESC` | Menu |

### Mục Tiêu Game

1. **Sinh tồn**: Giữ máu của bạn ở mức an toàn
2. **Tiêu diệt zombie**: Giết đủ số lượng zombie theo yêu cầu của stage
3. **Hoàn thành nhiệm vụ**: Thực hiện các objective để chiến thắng
4. **Tiến bộ**: Vượt qua các stage để tiếp tục game

### Tips

- 💡 Sử dụng sprint để tránh zombie khi cần
- 💡 Quản lý đạn cẩn thận, reload khi an toàn
- 💡 Sử dụng xe để di chuyển nhanh và tấn công zombie
- 💡 Hoàn thành objectives để unlock các stage tiếp theo

## 🏗️ Kiến Trúc Code

### Design Patterns

- **Singleton Pattern**: `GameManager`, `StageManager`, `UIManager`, `ObjectiveManager`
- **Object Pooling**: `SimplePool` class để tối ưu performance
- **ScriptableObject**: `WeaponData`, `StageData` để config dữ liệu
- **Observer Pattern**: Event system với `Action` và `event`

### Core Systems

#### GameManager
Quản lý trạng thái tổng thể của game, xử lý game over và win conditions.

#### StageManager
Điều phối progression giữa các stage, tracking kills và spawn requirements.

#### ObjectiveManager
Quản lý danh sách objectives, tracking completion và UI updates.

#### Spawner
Hệ thống spawn động dựa trên stage configuration với object pooling.

### Code Quality

- ✅ Separation of Concerns: Mỗi class có trách nhiệm rõ ràng
- ✅ Modular Design: Dễ dàng mở rộng và maintain
- ✅ Event-Driven: Sử dụng events để decouple components
- ✅ Configurable: Sử dụng ScriptableObjects cho data configuration

## 🔧 Tính Năng Kỹ Thuật

### Performance Optimization

- **Object Pooling**: Tái sử dụng zombie instances thay vì instantiate/destroy
- **NavMesh Caching**: Sử dụng NavMesh để tối ưu pathfinding
- **Efficient Spawning**: Spawn dựa trên max simultaneous count
- **UI Optimization**: Chỉ update UI khi cần thiết

### AI System

- **State Machine**: 3 states (Patrol, Chase, Attack)
- **Vision System**: Sphere cast để detect player
- **Pathfinding**: Unity NavMesh Agent
- **Attack Cooldown**: Prevent spam attacks

### Weapon System

- **Abstract Base Class**: `Weapon` class cho extensibility
- **ScriptableObject Data**: `WeaponData` cho easy configuration
- **Ammo Management**: Magazine và total ammo tracking
- **Reload System**: Animation và timing based

## 📸 Screenshots

> **Lưu ý**: Thêm screenshots của game vào thư mục `Screenshots/` và link vào đây

## 🤝 Đóng Góp

Mọi đóng góp đều được chào đón! Vui lòng:

1. Fork project
2. Tạo feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Mở Pull Request

## 📝 License

Project này được phân phối dưới giấy phép MIT. Xem file `LICENSE` để biết thêm chi tiết.

## 👨‍💻 Tác Giả

**Your Name**
- GitHub: [@yourusername](https://github.com/yourusername)
- Email: your.email@example.com
- LinkedIn: [Your LinkedIn](https://linkedin.com/in/yourprofile)

## 🙏 Lời Cảm Ơn

- Unity Technologies cho Unity Engine
- Các nhà phát triển assets đã sử dụng trong project
- Cộng đồng Unity Việt Nam

---

⭐ Nếu project này hữu ích, hãy cho một star trên GitHub!

📧 Liên hệ: your.email@example.com
