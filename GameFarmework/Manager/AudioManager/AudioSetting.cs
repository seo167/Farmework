namespace Farmework {
    public enum AudioState {
        AudioPlay=0,//播放
        AudioPause,//暂停
        AddVolume,//增加音量
        LessVolume,//减少音量
        LoopPlay,//循环播放
        CancelLoopPlay,//取消循环
        SequentialPlay,//顺序播放
        ShufflePlay,//随机播放
        Next,
        Prev,
		RepeatAudioPlay//音效在短时间内重复播放
    }

    public enum AudioUse {
       Single,//单次使用音效
       Repeat//多次使用音效
    }

    public enum AudioEffect {
        Normal,//正常
        ShadeBig,//渐变放大
        ShadeSmall,//渐变缩小
		DontDestroy//不删除
    }
}