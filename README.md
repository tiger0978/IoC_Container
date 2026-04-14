# Custom IoC Container 🛠️

這是一個輕量級、具備高度擴充性的 **Inversion of Control (IoC) Container**，使用 C# 刻板實作。
本專案的目的是為了深入探索 Dependency Injection (DI) 的底層運作原理、C# Reflection (反射) 的應用，並打造一套能無縫整合至自定義架構（如 MVP / MVVM）中的底層基礎設施。

## ✨ 核心功能 (Features)

* **生命週期管理 (Lifetime Management)**
  * `Transient`: 每次請求皆產生全新的實例。
  * `Singleton`: 整個應用程式生命週期內共用同一個實例。
* **依賴自動解析 (Constructor Injection)**
  * 支援遞迴解析，當建構子中包含其他介面或類別的依賴時，容器會自動向下尋找並完成實例化。
* **特性標籤自動註冊 (Attribute-based Auto Registration)**
  * 支援透過自訂標籤 `[Singleton]` 與 `[Transient]` 直接標記於類別上。
  * 容器啟動時可透過 Assembly 掃描，一鍵完成所有依賴注入，大幅減少手動撰寫 `AddService` 的樣板程式碼。
* **工廠模式整合 (Factory Pattern Integration)**
  * 內建 `PresenterFactory` 等實作，展示如何將 IoC 容器與 UI 架構模式完美結合。
