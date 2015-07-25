#include <stdlib.h>
#include <stdio.h>
#include <dlfcn.h>

void* dlopen(const char* file, int flag) {
    void* ret;
    ret = dlopen(file,flag);
    return ret;
}

char* dlerror() {
    char* err;
    err = dlerror();
    return err;
}

void* dlsym(void* handle, const char* symbol) {
    void* ret;
    ret = dlsym(handle,symbol); 
    return ret;
}

int dlclose(void* handle) {
    return dlclose(handle);
}
