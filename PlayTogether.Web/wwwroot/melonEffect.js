window.spawnClickText = (x, y, amount, critical) => {
    const text = document.createElement("div");
    text.className = "floating-click-text";
    text.textContent = "+" + amount;

    if (critical) {
        text.style.color = "#FFB700";
        text.style.fontSize = "2em";      // größer
        text.style.fontWeight = "bold";   // fetter
        text.style.textShadow = "0 0 10px red";
    } else {
        text.style.color = "limegreen";
    }

    const offsetX = (Math.random() - 0.5) * 30;
    text.style.left = (x + offsetX) + "px";
    text.style.top = y + "px";

    document.body.appendChild(text);

    setTimeout(() => text.remove(), 800);
};

window.spawnAutoClickText = (elementId, amount) => {
    const el = document.getElementById(elementId);
    if (!el) return;

    const rect = el.getBoundingClientRect();
    const centerX = rect.left + rect.width / 2;
    const centerY = rect.top;

    const text = document.createElement("div");
    text.className = "floating-autoclick-text";
    text.textContent = "+" + amount;

    const offsetX = (Math.random() - 0.5) * 40;
    text.style.left = (centerX + offsetX) + "px";
    text.style.top = centerY + "px";

    document.body.appendChild(text);

    setTimeout(() => text.remove(), 1000);
};