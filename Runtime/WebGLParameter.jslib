mergeInto(LibraryManager.library, {
    GetQueryParams: function() {
        try {
            var params = {}; // Use var instead of const or let for ES5 compatibility
            if (typeof window !== "undefined" && window.location && window.location.search) {
                var queryString = window.location.search;

                // Check if URLSearchParams exists (modern browsers)
                if (typeof URLSearchParams !== "undefined") {
                    var urlParams = new URLSearchParams(queryString);
                    urlParams.forEach(function(value, key) {
                        params[key] = value; // Populate the params object
                    });
                } else {
                    // Fallback for environments without URLSearchParams
                    var pairs = queryString.slice(1).split("&");
                    for (var i = 0; i < pairs.length; i++) {
                        var pair = pairs[i].split("=");
                        var key = decodeURIComponent(pair[0]);
                        var value = decodeURIComponent(pair[1] || "");
                        params[key] = value;
                    }
                }
            }

            // Convert params object to JSON string
            var paramsJson = JSON.stringify(params);
            var lengthBytes = lengthBytesUTF8(paramsJson) + 1; // Include null terminator
            var buffer = _malloc(lengthBytes);
            stringToUTF8(paramsJson, buffer, lengthBytes);
            return buffer;
        } catch (e) {
            console.error("Error in GetQueryParams:", e);
            var emptyJson = "{}";
            var lengthBytes = lengthBytesUTF8(emptyJson) + 1;
            var buffer = _malloc(lengthBytes);
            stringToUTF8(emptyJson, buffer, lengthBytes);
            return buffer;
        }
    },

    FreeQueryParams: function(ptr) {
        _free(ptr);
    }
});
