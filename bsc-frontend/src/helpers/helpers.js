export const toFormData = (data) => {
    const formData = new FormData();
    Object.keys(data).forEach((key) => formData.append(key, data[key]));
}

export const es = () => {
    console.log("s");
}