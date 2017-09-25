# 功能
 1. 使用TTUIFrame框架。
 2. 可以将做好的UItip prefab, 显示在screen空间中。
 3. 同一个UItip Prefab可以同时在screen中显示多个。
 4. 显示时，可以指定UItip prefab的screen坐标。

# 脚本实现
  1. `static TTUIPage ShowTip<T>(Vector3 screenPosition = default(Vector3), int maxNum = -1)`
   * T ：继承自TTUIPage 的一个类。也就是本次要显示的 UItip的 page类。
   * screenPosition： UItip的screen坐标，默认是（0,0,0）。
   * maxNum：屏幕上最多可显示多少个此种类型的UItip。
  2. `static void CloseTip<T>()`
    * T ：继承自TTUIPage 的一个类。也就是本次要显示的 UItip的 page类。
    * 关闭UItip，默认关闭最先显示的那个。
    * 后期如果有需要，可以指定需要关闭的gameobject.

# 辅助类
 1. MathUtil  （Assets\UI\Scripts\MathUtil)
  * ` Vector3 WorldToTTUIPosition(Vector3 worldPos, string achor="center")`
  * 将世界坐标转换到TTUIFrame中的屏幕坐标，并且做了一些修正。

# 使用举例
 1. 无人场景中，无人机在碰到金币时飘+1 UItip，碰到其他场景物飘-1 UItip.
 2. 制作UItip prefab，取名为：TipNumber
 3. 根据TTUIFrame框架规则，编写对应的TipNumber脚本。
 4. 在无人机碰撞逻辑中，根据碰撞物体不同，分别调用：
   * 碰到金币时： `(UITipHelper.ShowTip<TipNumber>(MathUtil.WorldToTTUIPosition(other.transform.position)) as TipNumber).SetNumber(1);`
   * 碰到其他障碍物：
   `(UITipHelper.ShowTip<TipNumber>() as TipNumber).SetNumber(-1)`

# 备注
目前TipNumber只支持显示一位数字。（+1~+9、-1~-9）。后期如果有需要可以扩展。 
