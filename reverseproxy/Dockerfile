FROM nginx
ARG NGINX_CONF
RUN echo $NGINX_CONF
ENV FILE="${NGINX_CONF}.nginx.conf"
COPY $FILE /etc/nginx/nginx.conf

