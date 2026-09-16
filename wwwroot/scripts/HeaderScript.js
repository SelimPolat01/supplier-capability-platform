async function logOutClickHandler() {
    const response = await fetch("/auth/log-out", { method: "POST", credentials: "include" });

    if (response.ok) {
        window.location.href = "/auth/login";
    } else {
        alert("An error occurred while logging out.")
    }
}
