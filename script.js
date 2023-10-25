"use strict";
console.log( navigator.usb.getDevices());
async function requestUSBPermission() {
    const filters = [
        { vendorId: 0x05BA, productId: 0x000A }
    ];

    try {
        const device = await navigator.usb.requestDevice({ filters: filters });
        await device.open();
        // Access and interact with the device here
        document.getElementById('status').textContent = `Status: Connected to ${device.productName}`;
    } catch (error) {
        console.error('Error connecting to USB device:', error);
        document.getElementById('status').textContent = 'Status: Connection failed';
    }
}

// Wait for the DOM to fully load
document.addEventListener('DOMContentLoaded', function() {
    const scanButton = document.querySelector('.scanButton');

    // Check if the WebUSB API is available in the browser
    if ('usb' in navigator) {
        // Add a click event listener to request USB permission
        scanButton.addEventListener('click', requestUSBPermission);
    } else {
        // Notify the user that the WebUSB API is not supported
        scanButton.textContent = 'WebUSB Not Supported';
        scanButton.disabled = true;
    }
});

