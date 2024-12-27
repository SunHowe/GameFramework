# GameFramework

#### 介绍
基于GameFramework框架提供FGUI、YooAsset、Luban的支持。（框架架构已为接入HybridCLR提供支持，但还未接入，后续补充）。


#### 框架说明

- 支持FGUI界面与自定义组件的控件静态绑定代码的生成，并在游戏初始化阶段自动完成URL与自定义组件的绑定。FGUI基于GameFramework原UI框架的抽象实现，方便使用者从UGUI框架迁移至FGUI框架（原UGUI框架并未剔除，若使用者需要使用UGUI框架也可自行使用）。

- 使用Luban配置工具替代原有GameFramework的配置表模块与本地化模块，对接Luban工具自动生成配置表代码的功能，并为配置表提供了预加载、懒加载、同步/异步等模式，可供开发者自行选择。

- 使用YooAsset资源管理模块(接入1.5.8版本)代替原有GameFramework的资源管理模块，将游戏资源包划分成启动器与游戏主资源包，开发者可根据自己的需要新增附加资源包。

- 剔除GameFramework中一些不太好用的模块(包含上述被其他插件替代的配置表、本地化、资源管理模块)，例如DataProvider等。

- 为GameFramework框架各组件提供基于UniTask的异步封装，方便开发者进行异步操作。

- 为GameFramework框架补充定时器(Timer)模块，并提供时间定时器、帧定时器(基于渲染帧)相关的功能支持。

- 为GameFramework框架补充游戏逻辑(GameLogic)模块，供开发者向GameFramework框架中添加、移除自定义的游戏逻辑的功能，当GameFramework框架被销毁时，会触发到当前注册的所有游戏逻辑的`Shutdown()`方法，以确保游戏逻辑能在游戏退出、重启时正确的被销毁。

- 为GameFramework框架补充安全区域(SafeArea)模块，提供获取安全区域、适配安全区域的功能，并支持Editor环境下自定义安全区域。模块设计符合GameFramework的Helper理念，开发者可以根据自己的需要替换成自己的SafeAreaHelper实现，用于支持运行时供玩家自行调整安全区域之类的特殊需求。

- 为GameFramework框架补充WidgetsGather和Widget组件，用于提供通用的收集场景或预制体上某些需要被游戏逻辑访问的组件的功能。并提供了相关的InspectorEditor支持，方便开发者使用。开发者既可以通过手动拖拽将组件挂载到某个WidgetsGather上，也可以通过挂载Widget组件，并声明名字和绑定的组件类型，然后通过WidgetsGather的面板进行自动收集绑定。运行时可以通过名字向WidgetsGather组件获取已注册的组件实例。

- 提供框架特色的Feature模块，基于EC(Entity-Component)的思想，将一些通用的逻辑封装成Feature，供开发者使用，并提供相关的拓展方法，基于懒初始化的思想在有使用到时才创建对应的Feature实例。目前游戏封装的GameLogicBase逻辑抽象类、FGUI界面逻辑抽象类、自定义组件、流程抽象类等模块中都已完成与Feature模块的对接，方便开发者使用。若开发者有其他类需要使用Feature功能，可自行维护FeatureContainer实例的生命周期或使用IFeatureOwner接口。目前框架支持的Feature功能有：

    - AsyncFeature：提供异步交互中所需的CancellationToken的管理，当Feature被销毁时，会自动将CancellationToken标记为取消。可以很方便的用于类似“界面打开时的异步交互，当界面被关闭时，自动取消异步操作”这样的使用场景。
    
    - DisposableGroupFeature: 提供对DisposableGroup实例的管理，当Feature被销毁时，会自动调用DisposableGroup实例的Disposa方法，将运行期间开发者添加到DisposableGroup中的IDisposable实例全部释放。可以很方便的用于类似“界面打开期间，添加了一些需要释放的资源，当界面被关闭时，自动释放资源”这样的使用场景。

    - EntityFeature: 提供对GameFramework实体实例的局部管理，当Feature被销毁时，会自动将由该Feature创建的实体实例全部销毁。

    - EventFeature: 提供对GameFramework事件的局部管理，当Feature被销毁时，会自动解除由该Feature注册的事件监听。可以很方便的用于类似“界面打开期间，注册了一些事件监听，当界面被关闭时，自动解除事件监听”这样的使用场景。并且可以方便地使用拓展方法`this.Fire(eventArgs)`进行事件派发。

    - FGUIFeature: 目前仅用于让开发者可以方便地使用拓展方法`this.OpenForm()`来打开界面。

    - SubGameLogicFeature: 提供子游戏逻辑实例的功能与管理，可供开发者自行附加子游戏逻辑实例，当Feature被销毁时，会自动销毁由该Feature创建的子游戏逻辑实例。例如一个玩法中，可能存在技能系统、战斗系统、任务系统等，这些系统可以单独作为一个游戏逻辑模块，当玩家进入该玩法时，创建对应的子游戏逻辑实例，当玩家退出该玩法时，自动销毁对应的子游戏逻辑实例。

    - GameObjectPoolFeature: 提供对GameFramework对象池的局部管理，在Feature运行期间，可以通过该Feature获取、归还指定资源的GameObject实例，当Feature被销毁时，会自动销毁由该Feature创建的GameObject实例。可以很方便的用于类似“界面打开期间，从对象池中获取了一些GameObject实例，当界面被关闭时，自动销毁这些GameObject实例”这样的使用场景。并提供了父级Feature的支持，若有指定父级Feature，父级Feature才是真正管理GameObject实例的Feature，子级Feature相当于一层代理，在子级Feature销毁时，会自动将子级Feature管理的GameObject实例归还给父级Feature。

    - ResourceFeature: 提供对GameFramework资源加载实例的局部管理，与GameObjectPoolFeature类似，在该Feature运行期间，可以通过该Feature加载、归还指定资源，当Feature被销毁时将资源归还给全局的ResourceManager。可以很方便的用于类似“界面打开期间，加载了一些资源，当界面被关闭时，自动归还这些资源”这样的使用场景。并提供了父级Feature的支持，若有指定父级Feature，父级Feature才是真正管理资源的Feature，子级Feature相当于一层代理，在子级Feature销毁时，会自动将子级Feature管理的资源归还给父级Feature。

    - TimerFeature: 提供对定时器模块的局部管理，当Feature被销毁时，会自动销毁由该Feature创建的定时器实例。可以很方便的用于类似“界面打开期间，创建了一些定时器，当界面被关闭时，自动销毁这些定时器”这样的使用场景。

    - ObjectCollectorFeature: 提供对ObjectCollector的拓展支持，可以在设置过ObjectCollector后，方便的通过`this.Get<T>(name)`获取到ObjectCollector中已注册的组件实例，配合`ObjectExport`组件可以在面板上快速的收集需要导出的组件实例，并且支持生成热更新层的静态绑定代码。。

    - FGUIFormFrameFeature: 为FGUI界面提供的界面框架功能，可以在界面初始化时进行添加该Feature(目前界面代码生成器会自动生成该Feature的添加代码，若不需要可以自行注释或删除)，该Feature会从界面上获取名字为`frame`的组件当做界面的框架组件，并从该框架组件上获取`closeButton`和`backButton`分别进行关闭按钮和返回按钮的逻辑处理。方便一些有通用界面框架的界面逻辑封装。比如全屏界面框架与弹窗界面框架。

    - FGUIFormSafeAreaFeature: 为FGUI界面提供的安全区域屏幕界面适配器，可以在界面初始化时添加该Feature，该Feature会让FGUI界面尺寸始终与安全区域尺寸相同且位置始终与安全区域位置相同。

    - FGUIFormConstantFeature: 为FGUI界面提供的固定尺寸界面适配器，可以在界面初始化时添加该Feature(目前界面代码生成器默认生成指定适配器，若不需要或需要修改可自行处理)，该Feature会让FGUI界面尺寸不随屏幕尺寸变化, 并提供位置适配功能(例如是否水平居中，是否垂直居中)。

    - FGUIFormFullScreenFeature: 为FGUI界面提供的全屏界面适配器，可以在界面初始化时添加该Feature，该Feature会让FGUI界面尺寸随屏幕尺寸变化, 提供安全区域适配功能，节点名固定为`safeArea`，`safeArea`节点的尺寸会始终与安全区域的位置和大小保持一致。


#### 相关框架、工具说明
- GameFramework: [https://github.com/EllanJiang/GameFramework](https://github.com/EllanJiang/GameFramework)
- UnityGameFramework: [https://github.com/EllanJiang/UnityGameFramework](https://github.com/EllanJiang/UnityGameFramework)
- FairyGUI: [https://www.fairygui.com](https://www.fairygui.com)
- FairyGUI-Dynamic: [https://github.com/SunHowe/FairyGUI-Dynamic](https://github.com/SunHowe/FairyGUI-Dynamic)
- FairyGUI-CodeGenerator: [https://github.com/SunHowe/FairyGUI-CodeGenerator](https://github.com/SunHowe/FairyGUI-CodeGenerator)
- YooAsset: [https://github.com/tuyoogame/YooAsset](https://github.com/tuyoogame/YooAsset)
- Luban: [https://github.com/focus-creative-games/luban](https://github.com/focus-creative-games/luban)
