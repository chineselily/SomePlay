
//修改Resources下的LocalizationText进行Key值绑定

//挂载Scripts下相应的脚本到需要本地化语言的物体上，并设置Key(建议中文)

//设置语言
if(Application.systemLanguage  == SystemLanguage.ChineseSimplified) 
{ 
	LocalizationText.SetLanguage("CN");
}