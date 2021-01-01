FROM nginx
ARG NGINX_CONF
RUN echo $NGINX_CONF
ENV FILE="${NGINX_CONF}.nginx.conf"
#RUN mkdir -p /usr/share/nginx/html/
#RUN echo "<html><head><title>STATUS</title></head><body><h1>Status:good</h1></body></html>" >> /usr/share/nginx/html/index.html
#RUN ls -la /usr/share/nginx/html/
#RUN cat /usr/share/nginx/html/index.html
COPY $FILE /etc/nginx/nginx.conf
