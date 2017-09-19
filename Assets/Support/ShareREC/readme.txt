
//挂载ShareREC

//ShareREC.IsAvailable();判断是否支持录屏

// 启动录制
ShareREC.StartRecorder();

// 暂停录制
ShareREC.PauseRecorder();

// 恢复录制
ShareREC.ResumeRecorder();

// 停止录制
ShareREC.StopRecorder();

//分享
ShareREC.SetText("视频描述");// 设置描述
ShareREC.ShowProfile();// 进入个人资料页面
ShareREC.ShowVideoCenter();// 进入应用视频列表页面
ShareREC.ShowShare();