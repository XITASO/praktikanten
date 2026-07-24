window.playClickSound = function () {
    const sound = new Audio("/sounds/click.mp3");
    sound.currentTime = 0;
    sound.play();
};