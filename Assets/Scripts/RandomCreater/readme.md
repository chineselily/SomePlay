# 功能
* 这是一个脚本，可将脚本挂到某个gameobject上。调用脚本的接口，可在某个范围内随机生产一些物体，并可将与其他collider发生碰撞的prefab删除。

# 脚本介绍
* 脚本暴露的到面板上可编辑的参数有:
 1. prefab To Create : 需要随机创建的prefab
 2. prefab Rotation: 创建出的prefab初始的Rotation
 3. Min Position: 创建范围中的最小值。（x,y,z）均是范围的最小值
 4. Max Position: 创建范围中的最大值。(x,y,z) 均是范围的最大值
 5. Max Num: 创建prefab的最大数量
 6. Min Distance: 随机创建的prefab之间的距离最小值
* 脚本变量：
  1. CreateCallBack callBack： 创建完成回调函数，每次调用执行接口RandomCreater::Create创建完物体后，都会回调这个函数。
  2. List<RandomCellData> randomObjects：prefab生成后保存到的池子。
* 执行接口：
 1. RandomCreater::Create()
   * 利用Lean.LeanPool 随机创建不多于Max Num个prefab，并保存到池子里。
   * 如果prefab与其他collider有碰撞，则删除这个prefab。
   * 如果池子里有被删除的prefab实例，刷新池子。
   * 回调 callBack。

# 使用举例
在无人机场景中随机生成一些金币prefab.
在场景的CoinParent gameobject上挂上RandomCreater脚本。暴露的参数设置如下：
1. prefab To Create : coin prfab
2. prefab Rotation: new Vector3（90,0，-180）
3. Min Position: new Vector3(-124,1,-94)
4. Max Position: new Vector3(73,25,103)
5. Max Num: 50
6. Min Distance: 3
在RandomCreater脚本的Start中就调用Create方法。所以进入场景就可以看到众多的金币。
