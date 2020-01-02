var request = new XMLHttpRequest();
request.open('GET', 'https://api.instagram.com/v1/users/self/media/recent/?access_token=6899479084.1677ed0.49fe1d1fb8154872bc1fa1c52dd529dd&count=50', true);
//request.open('GET', 'https://api.instagram.com/v1/users/self/media/recent/?access_token=2243792373.1677ed0.8db86b749f2145f5b4c3593fa81981b9&count=30', true);
request.onload = function (container) {
    container = document.getElementById('sliderTarget');
    if (container !== null) {
        if (request.status >= 200 && request.status < 400) {
            var data = JSON.parse(request.responseText);
            //console.log(container);
            // Success!
            var caption;
            for (var i = 0; i < data.data.length; i++) {
                for (var j = 0; j < data.data[i].tags.length; j++) {
                    if (data.data[i].tags[j] === 'брови') {
                        var imgURL = data.data[i].images.standard_resolution.url;
                        var li = document.createElement('li');
                        var divUkCard = document.createElement('div');
                        divUkCard.setAttribute('class', 'uk-card uk-card-default');
                        var divUkCardTop = document.createElement('div');
                        divUkCardTop.setAttribute('class', 'uk-card-media-top');
                        var img = document.createElement('img');
                        img.setAttribute('src', imgURL);
                        img.setAttribute('class', 'img');
                        divUkCardTop.appendChild(img);
                        divUkCard.appendChild(divUkCardTop);
                        li.appendChild(divUkCard);
                        
                        if (li !== null) {
                            container.appendChild(li);
                        }

                    }
                }
            }
        }
        // divMain.appendChild(div);
    }
    //console.log(container);
    //console.log(data);

};
request.onerror = function () {
    // There was a connection error of some sort
};
request.send();


 //if (data.data[i].caption && data.data[i].caption.text) {
            //    caption = data.data[i].caption.text;
            //} else {
            //    caption = 'no description';
            //}

            //  console.log(imgURL);


            //  console.log(data.data[i]);

            //for (var j = 0; j < data.data[i].tags.length; j++) {
            //    if (data.data[i].tags[j] === 'брови') {
            //        console.log('есть');
            //    }
            //}
